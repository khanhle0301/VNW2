(function (app) {
    app.controller('phucloiAddController', phucloiAddController);

    phucloiAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function phucloiAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.phucloi = {
            Status: true
        }

        $scope.AddPhucloi = AddPhucloi;

        function AddPhucloi() {
            apiService.post('/api/phucloi/create', $scope.phucloi, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.phucloi.Ten + ' đã được thêm mới.');
            $state.go('phuclois');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }

    }

})(angular.module('myshop.phuclois'));
