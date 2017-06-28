(function (app) {
    app.controller('nganhngheListController', nganhngheListController);

    nganhngheListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function nganhngheListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.nganhNghes = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getNganhNghes = getNganhNghes;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteNganhNghe = deleteNganhNghe;

        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.Id);
            });
            var config = {
                params: {
                    listId: JSON.stringify(listId)
                }
            }
            apiService.del('api/nganhnghe/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.nganhNghes, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.nganhNghes, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("nganhNghes", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteNganhNghe(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/nganhnghe/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getNganhNghes();
        }

        function getNganhNghes(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    keyword: $scope.keyword
                }
            }
            $scope.loading = true;
            apiService.get('/api/nganhnghe/getall', config, function (result) {
                $scope.nganhNghes = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                console.log('Load nganhnghe failed.');
                $scope.loading = false;
            });
        }

        $scope.getNganhNghes();
    }
})(angular.module('myshop.nganhnghes'));