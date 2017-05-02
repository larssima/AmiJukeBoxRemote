export class SortValueConverter {
    toView(array, propertyName, direction) {
        console.log('Sort', propertyName, direction);

        if (array == undefined)
            return [];

        if (propertyName === '')
            return array;

        var factor = direction === 'asc' ? 1 : -1;

        return array.slice(0).sort((a, b) => {
            if (a[propertyName] > b[propertyName]) return 1 * factor;
            if (a[propertyName] < b[propertyName]) return -1 * factor;
            if (a[propertyName] == b[propertyName]) return 0;
        });
    }
}