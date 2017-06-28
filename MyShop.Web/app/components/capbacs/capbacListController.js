(function (app) {
    app.controller('capbacListController', capbacListController);

    capbacListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function capbacListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.capbacs = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getCapbacs = getCapbacs;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteCapbac = deleteCapbac;

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
            apiService.del('api/capbac/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.capbacs, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.capbacs, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("capbacs", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteCapbac(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/capbac/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getCapbacs();
        }

        function getCapbacs(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    keyword: $scope.keyword
                }
            }
            $scope.loading = true;
            apiService.get('/api/capbac/getall', config, function (result) {
                $scope.capbacs = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                console.log('Load capbac failed.');
                $scope.loading = false;
            });
        }

        $scope.getCapbacs();
    }
})(angular.module('myshop.capbacs'));