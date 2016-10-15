function EvenTesting(val) {
    return val % 2 === 0;
}

function JustATest(val) {
    return true;
}

function GetCategory() {
    var xhr = $.ajax({
        type: 'POST',
        url: "http://localhost:3333/registerappapi/api/GetCategory",
        data: { "categoryID": 1 },
        datatype:"json",
        success:function(data) {
            return data;
        }
    });

    

}
