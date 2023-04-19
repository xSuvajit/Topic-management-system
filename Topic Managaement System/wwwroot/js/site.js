var alldata;
$(function () {
    $.ajax({
        url: "Home/GetSomeValue",
        success: (data) => {
            alldata = data;
        },
    });
});

const ShowValuesInConsole = () => {
    $.ajax({
        url: "Home/GetValue",
        success: (data) => {
            if (data) {
                console.log(data);
            }
            else {
                alert("no data");
            }
        }
    });
}

const Export_To_Excel = () => {
    $.ajax({
        url: "Home/ExportToExcel",
        success: (data) => {
            if (data) {
                console.log(data);
            }
            else {
                alert("no data");
            }
        }
    });
}