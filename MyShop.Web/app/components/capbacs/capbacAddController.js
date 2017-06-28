(function (app) {
    app.controller('capbacAddController', capbacAddController);

    capbacAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function capbacAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.capbac = {
            Status: true
        }

        $scope.AddCapbac = AddCapbac;

        function AddCapbac() {
            apiService.post('/api/capbac/create', $scope.capbac, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.capbac.Ten + ' đã được thêm mới.');
            $state.go('capbacs');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }

    }

})(angular.module('myshop.capbacs'));
