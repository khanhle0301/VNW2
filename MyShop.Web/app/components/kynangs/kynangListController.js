(function (app) {
    app.controller('kynangListController', kynangListController);

    kynangListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function kynangListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.kynangs = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getKynangs = getKynangs;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteKynang = deleteKynang;

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
            apiService.del('api/kynang/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.kynangs, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.kynangs, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("kynangs", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteKynang(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/kynang/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getKynangs();
        }

        function getKynangs(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    keyword: $scope.keyword
                }
            }
            $scope.loading = true;
            apiService.get('/api/kynang/getall', config, function (result) {
                $scope.kynangs = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                console.log('Load kynang failed.');
                $scope.loading = false;
            });
        }

        $scope.getKynangs();
    }
})(angular.module('myshop.kynangs'));