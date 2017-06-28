(function (app) {
    app.controller('phucloiListController', phucloiListController);

    phucloiListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function phucloiListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.phuclois = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getPhuclois = getPhuclois;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deletePhucloi = deletePhucloi;

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
            apiService.del('api/phucloi/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.phuclois, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.phuclois, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("phuclois", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deletePhucloi(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/phucloi/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getPhuclois();
        }

        function getPhuclois(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    keyword: $scope.keyword
                }
            }
            $scope.loading = true;
            apiService.get('/api/phucloi/getall', config, function (result) {
                $scope.phuclois = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                console.log('Load phucloi failed.');
                $scope.loading = false;
            });
        }

        $scope.getPhuclois();
    }
})(angular.module('myshop.phuclois'));