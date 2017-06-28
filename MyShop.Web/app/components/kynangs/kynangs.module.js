/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('myshop.kynangs', ['myshop.common']).config(config);

	config.$inject = ['$stateProvider', '$urlRouterProvider'];

	function config($stateProvider, $urlRouterProvider) {
		$stateProvider.state('kynangs', {
			url: "/kynangs",
			templateUrl: "/app/components/kynangs/kynangListView.html",
			parent: 'base',
			controller: "kynangListController"
		})
            .state('add_kynang', {
            	url: "/add_kynang",
            	parent: 'base',
            	templateUrl: "/app/components/kynangs/kynangAddView.html",
            	controller: "kynangAddController"
            })
            .state('edit_kynang', {
            	url: "/edit_kynang/:id",
            	templateUrl: "/app/components/kynangs/kynangEditView.html",
            	controller: "kynangEditController",
            	parent: 'base',
            });
	}
})();