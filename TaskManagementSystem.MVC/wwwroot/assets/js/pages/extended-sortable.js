var gridDemo = document.getElementById('gridDemo'),

    moveLeft = document.getElementById('move-left'),
    moveRight = document.getElementById('move-right'),

    moveHandleRight = document.getElementById('moveHandle-right'),
    moveHandleLeft = document.getElementById('moveHandle-left'),

    example1 = document.getElementById('example1'),

    example2Left = document.getElementById('example2-left'),
    example2Right = document.getElementById('example2-right'),

    example3Left = document.getElementById('example3-left'),
    example3Right = document.getElementById('example3-right'),

    example4 = document.getElementById('example4');

// Grid demo
new Sortable(gridDemo, {
    animation: 150,
    ghostClass: 'blue-background-class'
});

// Move Card
new Sortable(moveLeft, {
    group: 'shared', // set both lists to same group
    animation: 150
});

new Sortable(moveRight, {
    group: 'shared',
    animation: 150
});

//  Move with Handle
new Sortable(moveHandleLeft, {
    handle: '.handle', // handle class
    group: 'shared', // set both lists to same group
    animation: 150
});

new Sortable(moveHandleRight, {
    handle: '.handle', // handle class
    group: 'shared',
    animation: 150
});


// Simple list
new Sortable(example1, {
    animation: 150,
    ghostClass: 'blue-background-class'
});

// Cloning
new Sortable(example2Left, {
    group: {
        name: 'shared',
        pull: 'clone' // To clone: set pull to 'clone'
    },
    animation: 150
});

new Sortable(example2Right, {
    group: {
        name: 'shared',
        pull: 'clone'
    },
    animation: 150
});

// Example 3 - No Sorting
new Sortable(example3Left, {
    group: {
        name: 'shared',
        pull: 'clone',
        put: false // Do not allow items to be put into this list
    },
    animation: 150,
    sort: false // To disable sorting: set sort to false
});

new Sortable(example3Right, {
    group: 'shared',
    animation: 150
});

// Example 4 - Filter
new Sortable(example4, {
    filter: '.filtered',
    animation: 150
});