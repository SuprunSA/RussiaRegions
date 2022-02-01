import { apiMethods } from "../requests.js";
import { render } from "../renderHeader.js";
import { showDistricts, listenersHelper } from './districtModule.js';

window.addEventListener('load', () => {
    render();
    apiMethods.get('district')
        .then(result => showDistricts(result))
        .catch(error => console.warn(error));

    addListeners.sortDisable();
    addListeners.sortDistrictsButton();
    addListeners.searchByNameButton();
});

const addListeners = {
    sortDisable() {
        let orderBy = document.getElementById('order-by');
        let orderByWhat = document.getElementById('order-by-what')
        orderBy.addEventListener('change', () => {
            if (orderBy.value === 'none') {
                orderByWhat.setAttribute('disabled', undefined);
            } else {
                orderByWhat.removeAttribute('disabled');
            };
        });
    },

    sortDistrictsButton() {
        let selectAccept = document.getElementById('select-accept');
        selectAccept.addEventListener('click', (evt) => {
            evt.preventDefault();
            listenersHelper.removeCards();

            let path = 'district';
            let orderBy = document.getElementById('order-by');
            if (orderBy.value !== 'none') {
                let orderByWhat = document.getElementById('order-by-what');
                path += '?orderAsc=' + orderBy.value + '&orderBy=' + orderByWhat.value + '&order=true';
            };
            apiMethods.get(path)
                .then((result) => {
                    if (result.length === 0) {
                        listenersHelper.showNotFound();
                    } else showDistricts(result);
                })
                .catch((error) => {
                    console.warn(error);
                    listenersHelper.showNotFound();
                });
        });
    },

    searchByNameButton() {
        let search = document.getElementById('search');
        let searchName = document.getElementById('input-search-name');
        search.addEventListener('click', (evt) => {
            evt.preventDefault();
            listenersHelper.removeCards();

            let path = 'district/name/' + searchName.value;

            apiMethods.get(path)
                .then((result) => {
                    if (result.length === 0) {
                        listenersHelper.showNotFound();
                    } else showDistricts([result]);
                })
                .catch((error) => {
                    console.warn(error);
                    listenersHelper.showNotFound();
                });
        });

        searchName.addEventListener('input', () => {
            if (searchName.value) {
                search.removeAttribute('disabled');
            } else search.setAttribute('disabled', undefined);
        });
    },
};