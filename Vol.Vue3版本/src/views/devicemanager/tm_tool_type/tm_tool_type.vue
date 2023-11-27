<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/devicemanager/tm_tool_type/tm_tool_type.js此处编写
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
    import extend from "@/extension/devicemanager/tm_tool_type/tm_tool_type.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'tool_type_id',
                footer: "Foots",
                cnName: '易损件类型',
                name: 'tm_tool_type/tm_tool_type',
                url: "/tm_tool_type/",
                sortName: "tool_type_id"
            });
            const editFormFields = ref({"tool_type_code":"","tool_type_name":"","spec":"","remark":""});
            const editFormOptions = ref([[{"title":"易损件类型编码","field":"tool_type_code","type":"text"},
                               {"title":"易损件类型名称","field":"tool_type_name","type":"text"}],
                              [{"title":"型号","field":"spec","type":"text"},
                               {"title":"备注","field":"remark","type":"text"}]]);
            const searchFormFields = ref({"tool_type_code":"","tool_type_name":""});
            const searchFormOptions = ref([[{"title":"易损件类型编码","field":"tool_type_code","type":"text"},{"title":"易损件类型名称","field":"tool_type_name","type":"like"}]]);
            const columns = ref([{field:'tool_type_id',title:'易损件类型ID',type:'bigint',width:110,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'tool_type_code',title:'易损件类型编码',type:'string',link:true,width:150,align:'left',sort:true},
                       {field:'tool_type_name',title:'易损件类型名称',type:'string',width:220,align:'left'},
                       {field:'spec',title:'型号',type:'string',width:220,align:'left'},
                       {field:'status',title:'状态',type:'string',width:120,hidden:true,align:'left'},
                       {field:'remark',title:'备注',type:'string',width:220,align:'left'},
                       {field:'attr1',title:'预留字段1',type:'string',width:120,hidden:true,align:'left'},
                       {field:'attr2',title:'预留字段2',type:'string',width:220,hidden:true,align:'left'},
                       {field:'attr3',title:'预留字段3',type:'int',width:110,hidden:true,align:'left'},
                       {field:'attr4',title:'预留字段4',type:'int',width:110,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者ID',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'datetime',width:150,align:'left',sort:true},
                       {field:'ModifyID',title:'创建者ID',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Modifier',title:'更新者',type:'string',width:100,align:'left'},
                       {field:'ModifyDate',title:'更新时间',type:'datetime',width:150,align:'left',sort:true}]);
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
