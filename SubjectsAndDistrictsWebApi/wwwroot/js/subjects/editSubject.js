import { apiMethods } from '../requests.js';
import { render } from '../renderHeader.js';

window.addEventListener('load', () => {
    render();
    let code = new URLSearchParams(location.search).get('code');

    apiMethods.get('subject/' + code)
        .then((result) => {
            fillForm(result);
        })
        .catch((error) => {
            console.warn(error);
        });
});

function fillForm(subject) {
    let form = document.getElementById('edit-subject-form');
    let federalDistrict = document.getElementById('select-district');

    form[0].value = subject.code;
    form[1].value = subject.name;
    form[2].value = subject.adminCenterName;
    form[3].value = subject.population;
    form[4].value = subject.square;
    form[5].value = (subject.population / subject.square).toFixed(3);

    apiMethods.get('district')
        .then(result => fillSelectOptions(result, federalDistrict, subject.districtId))
        .catch(error => console.warn(error));

    form.addEventListener('submit', () => {
        apiMethods.put('subject', {
                code: +form[0].value,
                name: form[1].value,
                adminCenterName: form[2].value,
                population: +form[3].value,
                square: +form[4].value,
                districtId: +form[5].value
            })
            .then(() => location.pathname = 'index.html')
            .catch(error => console.warn(error));
    });
}

function fillSelectOptions(districts, select, selectedDistrict) {
    for (let district of districts) {
        let option = document.createElement('option');
        option.setAttribute('value', district.code);
        option.innerHTML = district.name + ', ' + district.code;
        if (selectedDistrict === district.code) {
            option.setAttribute('selected', undefined);
        }
        select.append(option);
    }
};