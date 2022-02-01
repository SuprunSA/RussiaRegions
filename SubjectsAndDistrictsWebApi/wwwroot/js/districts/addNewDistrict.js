import { apiMethods } from "../requests.js";
import { render } from "../renderHeader.js";

window.addEventListener('load', () => {
    render();

    let form = document.getElementById('add-district-form');
    form.addEventListener('submit', (evt) => {
        evt.preventDefault();

        apiMethods.post('district', {
                code: +form[0].value,
                name: form[1].value,
                subjects: []
            })
            .then(() => {
                location.pathname = '/html/indexDistricts.html';
                console.log('задание успешно провалено');
            })
            .catch(error => console.warn(error));
    });
});