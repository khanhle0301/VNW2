/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('myshop.nganhnghes', ['myshop.common']).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {
		$stateProvider.state('nganhnghes', {
			url: "/nganhnghes",
			templateUrl: "/app/components/nganhnghes/nganhngheListView.html",
			parent: 'base',
			controller: "nganhngheListController"
		})
            .state('add_nganhnghe', {
            	url: "/add_nganhnghe",
            	parent: 'base',
            	templateUrl: "/app/components/nganhnghes/nganhngheAddView.html",
            	controller: "nganhngheAddController"
            })
            .state('edit_nganhnghe', {
            	url: "/edit_nganhnghe/:id",
            	templateUrl: "/app/components/nganhnghes/nganhngheEditView.html",
            	controller: "nganhngheEditController",
            	parent: 'base',
            });
	}
})();