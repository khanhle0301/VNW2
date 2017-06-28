(function (app) {
    app.controller('nganhngheAddController', nganhngheAddController);

    nganhngheAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function nganhngheAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.nganhnghe = {
            Status: true
        }

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.nganhnghe.Alias = commonService.getSeoTitle($scope.nganhnghe.Ten);
        }

        $scope.AddNganhNghe = AddNganhNghe;

        function AddNganhNghe() {
            apiService.post('/api/nganhnghe/create', $scope.nganhnghe, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.nganhnghe.Ten + ' đã được thêm mới.');
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
    }

})(angular.module('myshop.nganhnghes'));
