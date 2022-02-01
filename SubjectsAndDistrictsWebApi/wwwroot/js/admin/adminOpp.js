import { render, contains } from '../renderHeader.js';
import { apiMethods } from '../requests.js';

window.addEventListener('load', () => {
    render();

    apiMethods.get('user')
        .then(result => fillUsers(result, document.getElementById('table-body')))
        .catch(error => console.warn(error));
});

function fillUsers(users, table) {
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
            name += user.middleName[0] + '.';
        };
        if (user.lastName) {
            name += user.lastName[0] + '.';
        };

        elems[2].innerText = name;

        if (!contains(user.roles, 'Admin')) {
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


            deleteButton.addEventListener('click', () => {
                let path = 'user/' + deleteButton.value;
                apiMethods.delete(path)
                    .then(() => {
                        deleteUser(deleteButton.value);
                        console.log('успешно удалён');
                    })
                    .catch(error => console.warn(error));
            });

            elems[3].classList.add('text-end');
            elems[3].append(deleteButton);
            elems[3].append(editRef);
        };

        table.append(row);
    };
};

function deleteUser(name) {
    let user = document.getElementById(name);
    user.remove();
}