import { apiMethods } from '../requests.js';
import { showSubjects, listenersHelper } from './subjectModule.js';
import { render } from '../renderHeader.js';

window.addEventListener('load', () => {
    render();
    apiMethods.get('subject/order')
        .then(result => showSubjects(result))
        .catch(error => console.warn(error));

    addListeners.sortDisable();
    addListeners.sortSubjectsButton();
    addListeners.filterNameButton();
    addListeners.filterDistrictsButton();
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

    sortSubjectsButton() {
        let selectAccept = document.getElementById('select-accept');
        selectAccept.addEventListener('click', (evt) => {
            evt.preventDefault();
            listenersHelper.removeCards();
            let path = 'subject/order' + listenersHelper.makeRequestParams();

            apiMethods.get(path)
                .then((result) => {
                    if (result.length === 0) {
                        listenersHelper.showNotFound();
                    } else showSubjects(result);
                })
                .catch((error) => {
                    console.warn(error);
                    showNotFound();
                });
        });
    },

    filterNameButton() {
        let filterByName = document.getElementById('filter-name');
        filterByName.addEventListener('click', (evt) => {
            evt.preventDefault();
            listenersHelper.removeCards();

            let filterName = document.getElementById('input-subject-name');
            let params = listenersHelper.makeRequestParams();
            let path = listenersHelper.makePath('subject/order', 'filter', filterName.value, params);

            apiMethods.get(path)
                .then((result) => {
                    if (result.length === 0) {
                        listenersHelper.showNotFound()
                    } else showSubjects(result);
                })
                .catch((error) => {
                    console.warn(error);
                    listenersHelper.showNotFound();
                });
        });
    },

    filterDistrictsButton() {
        let filterByDistrict = document.getElementById('filter-district');
        filterByDistrict.addEventListener('click', (evt) => {
            evt.preventDefault();
            listenersHelper.removeCards();

            let filterDistrict = document.getElementById('input-subject-district');
            let params = listenersHelper.makeRequestParams();
            let path = listenersHelper.makePath('subject/filterDistrict', 'name', filterDistrict.value, params);

            apiMethods.get(path)
                .then((result) => {
                    if (result.length === 0) {
                        listenersHelper.showNotFound()
                    } else showSubjects(result);
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

            let path = listenersHelper.makePath('subject/name', 'name', searchName.value, undefined);

            apiMethods.get(path)
                .then((result) => {
                    if (result.length === 0) {
                        listenersHelper.showNotFound();
                    } else showSubjects([result]);
                })
                .catch((error) => {
                    listenersHelper.showNotFound();
                    console.warn(error);
                });
        });

        searchName.addEventListener('input', () => {
            if (searchName.value) {
                search.removeAttribute('disabled');
            } else search.setAttribute('disabled', undefined);
        });
    },
};