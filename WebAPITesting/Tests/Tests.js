// when using chutzpah

/// <reference path="TestAPICalls.js"/>
/// <reference path="../Scripts/jquery-3.1.1.js"/>

test("Is Even Test", function () {
    ok(EvenTesting(0));
});

test("Just a Test", function () {
    ok(JustATest(0));
});

/// <reference path="TestAPICalls.js"/>
asyncTest("ajax call test", function () {
    stop(1000);
    ok(GetCategory());
    start();
});
