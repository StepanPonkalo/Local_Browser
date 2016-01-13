
          var myApp=angular.module('myApp',[]);
myApp.controller('myAppCtrl',
function ($scope,$http) {
    $http.get('localhost/../api/values/') .success(function(response){
        $scope.result=response;
    });
    $scope.up=function(index){ $http.get('localhost/../api/values?name='+index).success(function(response){
        $scope.result=response;})};
});
