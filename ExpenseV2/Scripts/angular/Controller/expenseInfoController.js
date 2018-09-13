    app.controller('expenseInfoController', function ($scope, $http) {

    window.test = $scope;

    $scope.request = { userId: window.sessionModel.userID, roleId: window.sessionModel.roleID };

    $scope.expenseInfoList = [];

    console.log($scope.request);

    getExpenses();

    function getExpenses() {

        return $http({
            method: "post",
            data: JSON.stringify($scope.request),
            url: window.actionUrls.getExpensesByUserID
        }).then(function successCallback(d) {

            console.log(d.data.ExpenseList);  //just checking

            $scope.expenseInfoList = d.data.ExpenseList;

        }, function errorCallback(d) {
            $scope.error = d.data;
            console.log("error: " + d.data);
        });

    }

});