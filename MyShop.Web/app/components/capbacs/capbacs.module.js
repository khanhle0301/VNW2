/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('myshop.capbacs', ['myshop.common']).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {
		$stateProvider.state('capbacs', {
			url: "/capbacs",
			templateUrl: "/app/components/capbacs/capbacListView.html",
			parent: 'base',
			controller: "capbacListController"
		})
            .state('add_capbac', {
            	url: "/add_capbac",
            	parent: 'base',
            	templateUrl: "/app/components/capbacs/capbacAddView.html",
            	controller: "capbacAddController"
            })
            .state('edit_capbac', {
            	url: "/edit_capbac/:id",
            	templateUrl: "/app/components/capbacs/capbacEditView.html",
            	controller: "capbacEditController",
            	parent: 'base',
            });
	}
})();