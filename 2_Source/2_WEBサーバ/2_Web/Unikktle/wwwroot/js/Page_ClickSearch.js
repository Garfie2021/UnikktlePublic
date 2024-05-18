// 関数名はjs全体でユニークじゃないとダメ。
// site.min.js に纏めれないjsは「Page_」付き。

$(function () {
    //console.log("_Layout.cshtml $(function ()");

    $('#ClickSearch').click(function (e) {
        // console.log("ClickSearch()");
        var SearchString = $('#ClickSearchText').val();
        // console.log("SearchString:" + SearchString);

        if (SearchString === "") {
            return false;
        }
        else {
            sessionStorage.setItem("SearchString", SearchString);
            sessionStorage.setItem("pageNum", 0);
        }
    });
});
