import { apiMethods } from "../requests.js";

export function showDistricts(districts) {
    let districtsContainer = document.getElementById('districts-container');

    for (let i = 0; i < districts.length; i++) {
        let div = document.createElement('div');
        div.classList.add('card');
        div.classList.add('text-center');
        div.classList.add('mb-3');
        div.setAttribute('id', districts[i].code);

        fillCard(districts[i], div);

        districtsContainer.append(div);
    };
};

function fillCard(district, div) {
    let cardTempl = document.getElementById('template');
    div.append(cardTempl.content.cloneNode(true));

    let cardText = div.getElementsByTagName('b');

    cardText[0].prepend(district.code);
    cardText[2].prepend(district.population);
    cardText[3].prepend(district.square);
    cardText[4].prepend(district.populationDencity.toFixed(3));
    let subjects = fillNames(district.subjects);
    cardText[1].prepend(subjects);

    let header = div.getElementsByClassName('card-header');
    header[0].prepend(district.name + ' федеральный округ');

    let link = div.getElementsByTagName('a');
    link[0].href += '?code=' + district.code;

    let button = div.getElementsByTagName('button');
    button[0].setAttribute('value', district.code);
    listenersHelper.deleteButton(button[0]);
};

function fillNames(subjects) {
    let subjectsNames = '-';

    if (subjects.length >= 1) {
        subjectsNames = subjects[0].name;
        for (let i = 1; i < subjects.length; i++) {
            subjectsNames += ', ' + subjects[i].name;
        };
    };

    return subjectsNames;
}

export const listenersHelper = {
    removeCards() {
        let cards = document.getElementsByClassName('card');
        cards = Array.from(cards);
        for (let card of cards) {
            card.remove();
        }
        let notFound = document.getElementById('not-found');
        if (notFound) {
            notFound.remove();
        }
    },

    removeCard(code) {
        let card = document.getElementById(code);
        card.remove();
    },

    showNotFound() {
        let input = document.getElementById('districts-container');
        let div = document.createElement('h5');
        div.classList.add('text-center');
        div.setAttribute('id', 'not-found');
        div.innerText = 'По вашему запросу ничего не найдено';

        input.append(div);
    },

    deleteButton(button) {
        button.addEventListener('click', () => {
            let code = button.value;
            let path = 'district/' + code;

            apiMethods.delete(path)
                .then(() => {
                    console.log('успешно удалён');
                    location.reload();
                })
                .catch(error => console.log(error));
        });
    },
}

//<div class="card text-center mb-3">