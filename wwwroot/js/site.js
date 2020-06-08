const uri = 'api/LockService';

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

// <AddItem>
function addItem() {
  const addNameTextbox = document.getElementById('add-name');

  const item = {
      Name: addNameTextbox.value.trim(),
  };
    
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'to-do' : 'Last locks weights that were modified';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('todos');
  tBody.innerHTML = '';
  _displayCount(data.length);

  data.forEach(item => {

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(item.name);
    td1.appendChild(textNode);
  });

  todos = data;
}
