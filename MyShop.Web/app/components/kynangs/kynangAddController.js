(function (app) {
    app.controller('kynangAddController', kynangAddController);

    kynangAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function kynangAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.kynang = {
            Status: true
        }

        $scope.AddKynang = AddKynang;

        function AddKynang() {
            apiService.post('/api/kynang/create', $scope.kynang, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.kynang.Ten + ' đã được thêm mới.');
            $state.go('kynangs');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }

    }

})(angular.module('myshop.kynangs'));
