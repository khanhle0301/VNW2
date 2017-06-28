(function (app) {
    app.controller('phucloiEditController', phucloiEditController);

    phucloiEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function phucloiEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.phucloi = {
            Status: true
        }

        $scope.UpdatePhucloi = UpdatePhucloi;

        function loadPhucloiDetail() {
            apiService.get('api/phucloi/getbyid/' + $stateParams.id, null, function (result) {
                $scope.phucloi = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdatePhucloi() {
            apiService.put('/api/phucloi/update', $scope.phucloi, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.phucloi.Ten + ' đã được cập nhật thành công.');
            $state.go('phuclois');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
        }

        loadPhucloiDetail();
    }

})(angular.module('myshop.phuclois'));