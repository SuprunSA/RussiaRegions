import { render, contains } from '../renderHeader.js';
import { apiMethods } from '../requests.js';

window.addEventListener('load', () => {
    render();

    apiMethods.get('user')
        .then(result => fillUsers(result, document.getElementById('table-body')))
        .catch(error => console.warn(error));
});

function fillUsers(users, table) {
    apiMethods.get('account')
        .then((result) => {
            fillTable(users, table, result.userName)
        })
        .catch(error => {
            console.warn(error);
        });
};

function deleteUser(name) {
    let user = document.getElementById(name);
    user.remove();
}

function fillTable(users, table, currentAdmin) {
    for (let user of users) {
        let rowContent = document.getElementById('user');
        let row = document.createElement('tr');
        row.setAttribute('id', user.userName);
        row.append(rowContent.content.cloneNode(true));
        let elems = row.getElementsByTagName('td');

        elems[0].innerText = user.userName;
        elems[1].innerText = user.email;
        let name = user.firstName;
        if (user.middleName) {
            name += ' ' + user.middleName[0] + '.';
        };
        if (user.lastName) {
            name += user.lastName[0] + '.';
        };

        elems[2].innerText = name;
        let role = '-';
        if (user.roles !== 0) {
            role = user.roles.toString();
        }
        elems[3].innerText = role;

        let editRef = document.createElement('a');
        let pencilIcon = document.createElement('i');
        pencilIcon.classList.add('bi');
        pencilIcon.classList.add('bi-pencil-fill');

        editRef.classList.add('btn');
        editRef.classList.add('btn-primary');
        editRef.classList.add('btn-sm');
        let href = './editUserByAdmin.html' + '?userName=' + user.userName;
        editRef.setAttribute('href', href);
        editRef.innerText = 'Редактировать';
        editRef.prepend(pencilIcon);

        let deleteButton = document.createElement('button');
        let trashIcon = document.createElement('i');
        trashIcon.classList.add('bi');
        trashIcon.classList.add('bi-trash-fill');

        deleteButton.classList.add('btn');
        deleteButton.classList.add('btn-danger');
        deleteButton.classList.add('btn-sm');
        deleteButton.setAttribute('value', user.userName);
        deleteButton.innerText = 'Удалить';
        deleteButton.prepend(trashIcon);

        if (currentAdmin === user.userName) {
            deleteButton.setAttribute('disabled', undefined);
        }

        deleteButton.addEventListener('click', () => {
            let path = 'user/' + deleteButton.value;
            apiMethods.delete(path)
                .then(() => {
                    deleteUser(deleteButton.value);
                    console.log('успешно удалён');
                })
                .catch(error => console.warn(error));
        });

        elems[4].classList.add('text-end');

        let div = document.createElement('div');
        div.classList.add('d-flex');
        div.classList.add('gap');
        div.classList.add('justify-content-end');

        div.append(editRef);
        div.append(deleteButton);
        elems[4].append(div);

        table.append(row);
    };
}