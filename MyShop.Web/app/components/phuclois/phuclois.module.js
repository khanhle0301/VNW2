/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('myshop.phuclois', ['myshop.common']).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {
		$stateProvider.state('phuclois', {
			url: "/phuclois",
			templateUrl: "/app/components/phuclois/phucloiListView.html",
			parent: 'base',
			controller: "phucloiListController"
		})
            .state('add_phucloi', {
            	url: "/add_phucloi",
            	parent: 'base',
            	templateUrl: "/app/components/phuclois/phucloiAddView.html",
            	controller: "phucloiAddController"
            })
            .state('edit_phucloi', {
            	url: "/edit_phucloi/:id",
            	templateUrl: "/app/components/phuclois/phucloiEditView.html",
            	controller: "phucloiEditController",
            	parent: 'base',
            });
	}
})();