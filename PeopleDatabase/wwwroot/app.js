Ext.onReady(function () {
    var store = Ext.create('Ext.data.Store', {
        autoLoad: true,
        fields: ['id', 'name', 'dateOfBirth', 'phone'],
        proxy: {
            type: 'ajax',
            url: '/api/persons',
            reader: {
                type: 'json'
            }
        }
    });

    Ext.create('Ext.grid.Panel', {
        renderTo: Ext.getBody(),
        store: store,
        title: 'Информация о людях',
        columns: [
            {
                xtype: 'actioncolumn',
                align: 'center',
                width: 40, 
                items: [{
                    icon: './images/delete.png',
                    tooltip: 'Delete',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        Ext.Ajax.request({
                            url: '/api/Persons/' + record.get('id'),
                            method: 'DELETE',
                            success: function (response) {
                                grid.getStore().remove(record);
                            },
                            //failure: function (response) { }
                        });
                    }
                }]
            },
            { text: 'ID', dataIndex: 'id', width: 50, align: 'center', textAlign: 'center' },
            { text: 'ФИО', dataIndex: 'name', editor: 'textfield', width: 300, align: 'center', textAlign: 'center' },
            { text: 'Дата рождения', dataIndex: 'dateOfBirth', editor: 'datefield', renderer: Ext.util.Format.dateRenderer('m/d/Y'), width: 100, align: 'center', textAlign: 'center' },
            { text: 'Номер телефона', dataIndex: 'phone', editor: 'textfield', width: 160, align: 'center', textAlign: 'center' }
        ],
        tbar: [{
            text: 'Добавить запись',
            handler: function () {
                var store = this.up('grid').getStore();
                var newPerson = {
                    name: 'Пупкин Василий Иванович',
                    dateOfBirth: new Date(1970,0,1),
                    phone: 'N/A'
                };
                var model = store.add(newPerson)[0];

                Ext.Ajax.request({
                    url: '/api/persons',
                    method: 'POST',
                    jsonData: newPerson,
                    success: function (response) {
                        var responseData = Ext.decode(response.responseText);
                        model.set('id', responseData.id);
                    },
                    //failure: function (response) { }
                });
            }
        }],
        plugins: [{
            ptype: 'cellediting',
            clicksToEdit: 1,
            listeners: {
                edit: function (editor, context) {
                    Ext.Ajax.request({
                        url: '/api/persons/' + context.record.get('id'),
                        method: 'PUT',
                        jsonData: {
                            name: context.record.get('name'),
                            dateOfBirth: context.record.get('dateOfBirth'),
                            phone: context.record.get('phone')
                        },
                        success: function (response) {
                            context.record.commit();
                        }
                    });
                }
            }
        }]
    });
});
