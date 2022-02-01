import { apiMethods } from "../requests.js";

export function showSubjects(subjects) {
    let length = subjects.length;
    if (length % 2) {
        length -= 1;
    };

    let subjectsContainer = document.getElementById('subjects-container');

    for (let i = 0; i < length;) {
        let row = document.createElement('div');
        row.classList.add('row');

        fillCard(subjects[i], row, false);
        fillCard(subjects[i + 1], row, false);

        subjectsContainer.append(row);
        i += 2;
    }

    if (length != subjects.length) {
        let centeredRow = document.createElement('div');
        centeredRow.classList.add('row');
        centeredRow.classList.add('justify-content-center');
        fillCard(subjects[length], centeredRow, true);
        subjectsContainer.append(centeredRow);
    }
};

function fillCard(subject, row, last) {
    let cardTempl = document.getElementById('template');
    let col = document.createElement('div');
    col.setAttribute('id', subject.code);

    if (last) {
        col.classList.add('col-6');
    } else {
        col.classList.add('col');
    }

    col.append(cardTempl.content.cloneNode(true));

    let cardText = col.getElementsByTagName('b');

    cardText[0].prepend(subject.code);
    apiMethods.get('district/' + subject.districtId)
        .then(result => cardText[1].prepend(result.name))
        .catch(error => console.warn(error));
    cardText[2].prepend(subject.adminCenterName);
    cardText[3].prepend(subject.population);
    cardText[4].prepend(subject.square);
    cardText[5].prepend((subject.population / subject.square).toFixed(3));

    let cardHeader = col.getElementsByTagName('h5');
    cardHeader[0].prepend(subject.name);

    let link = col.getElementsByTagName('a');
    link[0].href += '?code=' + subject.code;

    let button = col.getElementsByTagName('button');
    button[0].setAttribute('value', subject.code);
    listenersHelper.deleteSubject(button[0]);

    row.append(col);
};

export const listenersHelper = {
    makeRequestParams() {
        let orderBy = document.getElementById('order-by');
        let orderByWhat = document.getElementById('order-by-what');
        let params = '';
        if (orderBy.value != 'none') {
            params += '?orderAsc=' + orderBy.value + '&orderBy=' + orderByWhat.value + '&order=true';
        }
        return params;
    },

    makePath(search, currentParam, value, params) {
        let path = search;
        if (params && value) {
            path += params + '&' + currentParam + '=' + value;
        } else if (value) {
            path += '?' + currentParam + '=' + value;
        }
        return path;
    },

    removeCards() {
        let cards = document.getElementsByClassName('row');
        cards = Array.from(cards);
        cards.splice(0, 1);
        for (let card of cards) {
            card.remove();
        }
        let notFound = document.getElementById('not-found');
        if (notFound) {
            notFound.remove();
        }
    },

    removeCard(id) {
        let card = document.getElementById(id);
        card.remove();
    },

    deleteSubject(button) {
        button.addEventListener('click', () => {
            let code = button.value;
            let path = 'subject/' + code;

            apiMethods.delete(path)
                .then(() => {
                    console.log('успешно удалён');
                    location.reload();
                })
                .catch(error => console.warn(error));
        });
    },

    showNotFound() {
        let input = document.getElementById('subjects-container');
        let div = document.createElement('h5');
        div.classList.add('text-center');
        div.setAttribute('id', 'not-found');
        div.innerText = 'По вашему запросу ничего не найдено';

        input.append(div);
    }
};