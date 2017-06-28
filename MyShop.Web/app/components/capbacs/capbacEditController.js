(function (app) {
    app.controller('capbacEditController', capbacEditController);

    capbacEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function capbacEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.capbac = {
            Status: true
        }

        $scope.UpdateCapbac = UpdateCapbac;

        function loadCapbacDetail() {
            apiService.get('api/capbac/getbyid/' + $stateParams.id, null, function (result) {
                $scope.capbac = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateCapbac() {
            apiService.put('/api/capbac/update', $scope.capbac, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.capbac.Ten + ' đã được cập nhật thành công.');
            $state.go('capbacs');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }

        loadCapbacDetail();
    }

})(angular.module('myshop.capbacs'));