(function (app) {
    app.controller('kynangEditController', kynangEditController);

    kynangEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function kynangEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.kynang = {
            Status: true
        }

        $scope.UpdateKynang = UpdateKynang;

        function loadKynangDetail() {
            apiService.get('api/kynang/getbyid/' + $stateParams.id, null, function (result) {
                $scope.kynang = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateKynang() {
            apiService.put('/api/kynang/update', $scope.kynang, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.kynang.Ten + ' đã được cập nhật thành công.');
            $state.go('kynangs');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }

        loadKynangDetail();
    }

})(angular.module('myshop.kynangs'));