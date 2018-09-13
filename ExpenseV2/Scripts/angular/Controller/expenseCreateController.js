
app.controller('expenseCreateController', function ($scope, $http) {

    window.test = $scope;

    var currentExpenseID = window.expenseId;    //currentExpenseID holds Expense ID

    var sessionModel = window.sessionModel;     //sessionModel holds Session info

    console.log(sessionModel);
    console.log(currentExpenseID);

    $scope.request = {};

    $scope.request.expenseID = window.expenseId;

    $scope.rejectReason = "";

    $scope.expenseItem = {
        expenseItemDescription: "",
        expenseItemAmount: "",
        expenseItemDate: ""
    };

    $scope.expenseItemList = [];

    $scope.showReject = false;

    console.log($scope.showReject);

    $scope.showRejectDesc = function () {     //REJECT DESCRIPTION
        $scope.showReject = true;
        console.log($scope.showReject);
    }

    if (currentExpenseID > 0) {
        getExpenseDetails();
    } else {
        $scope.expenseItemList = [{ expenseItemDescription: "", expenseItemAmount: "", expenseItemDate: "" }];
    }

    $scope.addItem = function () {  //ADD ITEM
        $scope.expenseItemList.push({ expenseItemDescription: "", expenseItemAmount: "", expenseItemDate: "" });
    };

    $scope.deleteItem = function (index) {   //DELETE ITEM
        $scope.expenseItemList.splice(index, 1);
        console.log(index);
    };




    function getExpenseDetails() {   //GET DETAILS

        return $http({
            method: "post",
            data: JSON.stringify($scope.request),
            url: window.actionUrls.getExpensesByExpenseID
        }).then(function successCallback(d) {

            $scope.expenseItemList = d.data.ExpenseItemList;
            console.log("This is what's coming: " + d.data);

        }, function errorCallback(d) {
            $scope.error = d.data;
        });

    };

    function formatDate(date, isComingFromDb) {   //Date format conversion
        var dateString = date;
        if (isComingFromDb) {
            var momentDateObj = moment(dateString, 'YYYY-MM-DD');
            var momentDateString = momentDateObj.format('DD.MM.YYYY').split("T")[0];
            date = momentDateString;
            return date;
        }
        else {
            var momentDateObj = moment(dateString, 'DD.MM.YYYY');
            var momentDateString = momentDateObj.format().split("+")[0];
            date = momentDateString;
            return date;
        }
    }


    $scope.submitExpense = function () {    //SUBMIT

        console.log($scope.request);

        $scope.request.expenseItemList = $scope.expenseItemList;

        return $http({
            method: "post",
            data: JSON.stringify($scope.request),
            url: window.actionUrls.submitExpenseToDatabase
        }).then(function successCallback(d) {
            if (d.data.isSuccess) {
                console.log(d.data.message);    //do sth else with this

            } else {
                console.log("Failed: " + d.data.message);
            }

        }, function errorCallback(d) {
            $scope.error = d.data;
            console.log("error: " + d.data);
        });

        $location.href = '/index.html';  //Redirect                 DOESN'T WORK
    };



    $scope.evaluateExpense = function (isApproved, rejectDesc) {      //EVALUATE

        var evaluateResponse = {
            expenseID: currentExpenseID,
            approved: isApproved,
            rejectDescription: rejectDesc
        }

        console.log(evaluateResponse);

        return $http({
            method: "post",
            data: JSON.stringify(evaluateResponse),
            url: window.actionUrls.evaluateExpense
        }).then(function successCallback(d) {
            if (d.data.IsSuccess) {
                console.log(d.data.message);    //do sth else with this
            } else {
                console.log("Attempt Failed: " + d.data.message);
            }

        }, function errorCallback(d) {
            $scope.error = d.data;
            console.log("error: " + d.data);
        });
    };
});