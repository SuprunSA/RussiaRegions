import { apiMethods } from "../requests.js";
import { render } from '../renderHeader.js';

window.addEventListener('load', () => {
    render();
    getDistricts();
    addListeners.addNewSubjectButton();
});

function getDistricts() {
    let federalDistrict = document.getElementById('select-district');

    apiMethods.get('district')
        .then(result => fillSelectOptions(result, federalDistrict))
        .catch(error => console.warn(error));

    federalDistrict.addEventListener('change', (evt) => {
        evt.preventDefault();
        let submit = document.getElementById('submit');
        if (federalDistrict.value === 'none') {
            submit.setAttribute('disabled', undefined);
        } else submit.removeAttribute('disabled');
    })
};

function fillSelectOptions(districts, select) {
    for (let district of districts) {
        let option = document.createElement('option');
        option.setAttribute('value', district.code);
        option.innerHTML = district.name + ', ' + district.code;
        select.append(option);
    }
};

const addListeners = {
    addNewSubjectButton() {
        let form = document.getElementById('new-subject-form');
        form.addEventListener('submit', (evt) => {
            evt.preventDefault();
            const form = evt.target;
            let path = 'subject';

            apiMethods.post(path, {
                    code: +form[0].value,
                    name: form[1].value,
                    adminCenterName: form[2].value,
                    population: +form[3].value,
                    square: +form[4].value,
                    districtId: +form[5].value
                })
                .then(() => {
                    location.pathname = 'index.html';
                    console.log('успешно добавлен');
                })
                .catch((error) => {
                    console.warn(error);
                });
        });
    }
};