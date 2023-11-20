<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/devicemanager/dv_machinery_type/dv_machinery_type.js此处编写
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
    import extend from "@/extension/devicemanager/dv_machinery_type/dv_machinery_type.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'machinery_type_id',
                footer: "Foots",
                cnName: '设备类型',
                name: 'dv_machinery_type/dv_machinery_type',
                url: "/dv_machinery_type/",
                sortName: "machinery_type_id"
            });
            const editFormFields = ref({"machinery_type_code":"","machinery_type_name":"","parent_type_id":[],"enable_flag":"","remark":""});
            const editFormOptions = ref([[{"title":"设备类型编码","required":true,"field":"machinery_type_code"},
                               {"title":"设备类型名称","required":true,"field":"machinery_type_name"}],
                              [{"dataKey":"设备类型级联","data":[],"title":"父类型ID","field":"parent_type_id","type":"cascader"},
                               {"dataKey":"enable","data":[],"title":"是否启用","required":true,"field":"enable_flag","type":"select"}],
                              [{"title":"备注","field":"remark"}]]);
            const searchFormFields = ref({"machinery_type_code":"","machinery_type_name":"","enable_flag":""});
            const searchFormOptions = ref([[{"title":"设备类型编码","field":"machinery_type_code","type":"like"},{"title":"设备类型名称","field":"machinery_type_name","type":"like"}],[{"dataKey":"enable","data":[],"title":"是否启用","field":"enable_flag","type":"select"}]]);
            const columns = ref([{field:'machinery_type_id',title:'设备类型ID',type:'bigint',width:110,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'machinery_type_code',title:'设备类型编码',type:'string',width:120,require:true,align:'left',sort:true},
                       {field:'machinery_type_name',title:'设备类型名称',type:'string',width:220,require:true,align:'left'},
                       {field:'parent_type_id',title:'父类型ID',type:'bigint',bind:{ key:'设备类型级联',data:[]},width:110,hidden:true,align:'left'},
                       {field:'enable_flag',title:'是否启用',type:'string',bind:{ key:'enable',data:[]},width:110,require:true,align:'left'},
                       {field:'remark',title:'备注',type:'string',width:220,align:'left'},
                       {field:'attr1',title:'预留字段1',type:'string',width:120,hidden:true,align:'left'},
                       {field:'attr2',title:'预留字段2',type:'string',width:220,hidden:true,align:'left'},
                       {field:'attr3',title:'预留字段3',type:'int',width:110,hidden:true,align:'left'},
                       {field:'attr4',title:'预留字段4',type:'int',width:110,hidden:true,align:'left'},
                       {field:'create_by',title:'创建者',type:'string',width:120,hidden:true,align:'left'},
                       {field:'create_time',title:'创建时间',type:'datetime',width:150,hidden:true,align:'left',sort:true},
                       {field:'update_by',title:'更新者',type:'string',width:120,hidden:true,align:'left'},
                       {field:'update_time',title:'更新时间',type:'datetime',width:150,hidden:true,align:'left',sort:true}]);
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
