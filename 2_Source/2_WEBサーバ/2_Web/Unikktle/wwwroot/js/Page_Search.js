// 関数名はjs全体でユニークじゃないとダメ。
// site.min.js に纏めれないjsは「Page_」付き。

//document.addEventListener('DOMContentLoaded', function () {
$(function () {
    sessionStorage.setItem("pageNum", 1);
});

function ClickExternalSearch(url) {
    window.open(url, '_blank');
}
