// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById('load').addEventListener('click', () => {
    var xhr = new XMLHttpRequest();
    var elem = document.getElementById("elem").value;
    xhr.open('DELETE', '/Home/DeleteFromDataBase/?id=' + elem);
    xhr.send();
    console.log(`Элемент {id = ${elem}} удален.`);
});