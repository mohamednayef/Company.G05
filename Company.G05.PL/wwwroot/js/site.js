// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

    let SearchInput = document.getElementById('SearchInput');
    let ResultsDiv = document.getElementById('ResultsDiv');

    SearchInput.addEventListener('keyup', (e) => {
    e.preventDefault(); // يمنع أي submit يحصل

    let xhr = new XMLHttpRequest();
    let url = `${window.location.pathname}?SearchInput=${encodeURIComponent(SearchInput.value)}`;

    xhr.open('GET', url, true);

    xhr.onreadystatechange = () => {
    if (xhr.readyState === 4 && xhr.status === 200) {
    ResultsDiv.innerHTML = xhr.responseText; // حط النتيجة في الديف بدل ما تطبعها
}
};

    xhr.send();
});