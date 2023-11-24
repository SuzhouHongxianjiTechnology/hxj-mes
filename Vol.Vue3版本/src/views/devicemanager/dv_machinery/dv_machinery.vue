<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/devicemanager/dv_machinery/dv_machinery.js此处编写
 -->
<template>
    <view-grid ref="grid"
               :columns="columns"
               :detail="detail"
               :editFormFields="editFormFields"
               :editFormOptions="editFormOptions"
               :searchFormFields="searchFormFields"
               :searchFormOptions="searchFormOptions"
               :table="table"
               :extend="extend">
    </view-grid>
</template>
<script>
    import extend from "@/extension/devicemanager/dv_machinery/dv_machinery.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'machinery_id',
                footer: "Foots",
                cnName: '设备台账',
                name: 'dv_machinery/dv_machinery',
                url: "/dv_machinery/",
                sortName: "machinery_id"
            });
            const editFormFields = ref({"machinery_code":"","machinery_name":"","machinery_brand":"","machinery_spec":"","machinery_type_name":"","workshop_name":"","machinery_ip":"","remark":"","machinery_config":""});
            const editFormOptions = ref([[{"title":"设备编码","required":true,"field":"machinery_code","type":"text"},
                               {"title":"设备名称","required":true,"field":"machinery_name","type":"text"}],
                              [{"title":"品牌","field":"machinery_brand"},
                               {"title":"规格型号","field":"machinery_spec"}],
                              [{"title":"设备类型名称","field":"machinery_type_name"},
                               {"title":"所属车间名称","field":"workshop_name"}],
                              [{"title":"设备通讯接口","required":true,"field":"machinery_ip","type":"text"},
                               {"title":"备注","field":"remark","type":"text"}],
                              [{"title":"设备配置信息","field":"machinery_config","colSize":12,"type":"editor"}]]);
            const searchFormFields = ref({"machinery_code":"","machinery_name":""});
            const searchFormOptions = ref([[{"title":"设备编码","field":"machinery_code","type":"like"},{"title":"设备名称","field":"machinery_name","type":"text"}]]);
            const columns = ref([{field:'machinery_id',title:'设备ID',type:'bigint',width:120,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'machinery_code',title:'设备编码',type:'string',width:120,require:true,align:'left',sort:true},
                       {field:'machinery_name',title:'设备名称',type:'string',width:120,require:true,align:'left'},
                       {field:'machinery_brand',title:'品牌',type:'string',width:120,align:'left'},
                       {field:'machinery_spec',title:'规格型号',type:'string',width:120,align:'left'},
                       {field:'machinery_type_id',title:'设备类型ID',type:'bigint',width:120,hidden:true,require:true,align:'left'},
                       {field:'machinery_type_code',title:'设备类型编码',type:'string',width:120,hidden:true,align:'left'},
                       {field:'machinery_type_name',title:'设备类型名称',type:'string',width:120,align:'left'},
                       {field:'machinery_ip',title:'设备通讯接口',type:'string',width:120,require:true,align:'left'},
                       {field:'machinery_config',title:'设备配置信息',type:'string',width:120,align:'left'},
                       {field:'workshop_id',title:'所属车间ID',type:'bigint',width:120,hidden:true,require:true,align:'left'},
                       {field:'workshop_code',title:'所属车间编码',type:'string',width:120,hidden:true,align:'left'},
                       {field:'workshop_name',title:'所属车间名称',type:'string',width:120,align:'left'},
                       {field:'status',title:'设备状态',type:'string',bind:{ key:'设备状态',data:[]},width:120,require:true,align:'left'},
                       {field:'remark',title:'备注',type:'string',width:120,align:'left'},
                       {field:'attr1',title:'预留字段1',type:'string',width:120,hidden:true,align:'left'},
                       {field:'attr2',title:'预留字段2',type:'string',width:120,hidden:true,align:'left'},
                       {field:'attr3',title:'预留字段3',type:'int',width:120,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者ID',type:'int',width:120,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:120,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'datetime',width:120,align:'left',sort:true},
                       {field:'ModifyID',title:'更新者ID',type:'int',width:120,hidden:true,align:'left'},
                       {field:'Modifier',title:'更新者',type:'string',width:120,align:'left'},
                       {field:'ModifyDate',title:'更新时间',type:'datetime',width:120,align:'left',sort:true}]);
            const detail = ref({
                cnName: "#detailCnName",
                table: "#detailTable",
                columns: [],
                sortName: "",
                key: ""
            });
            return {
                table,
                extend,
                editFormFields,
                editFormOptions,
                searchFormFields,
                searchFormOptions,
                columns,
                detail,
            };
        },
    });
</script>
