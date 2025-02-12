var kanbanOne = document.getElementById('kanbanborad-one'),
    kanbanTwo = document.getElementById('kanbanborad-two'),
    kanbanThree = document.getElementById('kanbanborad-three'),
    kanbanFour = document.getElementById('kanbanborad-four');


new Sortable(kanbanOne, {
    group: 'shared',
    animation: 150
});

new Sortable(kanbanTwo, {
    group: 'shared',
    animation: 150
});

new Sortable(kanbanThree, {
    group: 'shared',
    animation: 150
});

new Sortable(kanbanFour, {
    group: 'shared',
    animation: 150
});

