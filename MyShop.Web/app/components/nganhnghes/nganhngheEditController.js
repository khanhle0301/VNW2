(function (app) {
    app.controller('nganhngheEditController', nganhngheEditController);

    nganhngheEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function nganhngheEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.nganhnghe = {
            Status: true
        }

        $scope.UpdateNganhNghe = UpdateNganhNghe;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.nganhnghe.Alias = commonService.getSeoTitle($scope.nganhnghe.Ten);
        }

        function loadNganhNgheDetail() {
            apiService.get('api/nganhnghe/getbyid/' + $stateParams.id, null, function (result) {
                $scope.nganhnghe = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateNganhNghe() {
            apiService.put('/api/nganhnghe/update', $scope.nganhnghe, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.nganhnghe.Ten + ' đã được cập nhật thành công.');
            $state.go('nganhnghes');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }

        function loadParentCategory() {
            apiService.get('api/nganhnghe/getallparents', null, function (result) {
                $scope.parentNganhNghes = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentCategory();
        loadNganhNgheDetail();
    }

})(angular.module('myshop.nganhnghes'));