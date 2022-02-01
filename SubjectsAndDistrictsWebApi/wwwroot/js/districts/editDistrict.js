import { apiMethods } from '../requests.js';
import { render } from '../renderHeader.js';

window.addEventListener('load', () => {
    render();

    let code = new URLSearchParams(location.search).get('code');
    apiMethods.get('district/' + code)
        .then(result => fillForm(result))
        .catch(error => console.log(error));


});

function fillForm(district) {
    let form = document.getElementById('edit-district-form');

    form[0].value = district.code;
    form[1].value = district.name;

    form.addEventListener('submit', () => {
        apiMethods.put('district?code=' + form[0].value + '&name=' + form[1].value)
            .then(() => location.pathname = 'html/indexDistricts.html')
            .catch((error) => console.warn(error));
    });
};