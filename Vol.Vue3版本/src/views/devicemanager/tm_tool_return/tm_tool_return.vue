<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/devicemanager/tm_tool_return/tm_tool_return.js此处编写
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
    import extend from "@/extension/devicemanager/tm_tool_return/tm_tool_return.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'tool_tool_return_id',
                footer: "Foots",
                cnName: '易损件借还',
                name: 'tm_tool_return/tm_tool_return',
                url: "/tm_tool_return/",
                sortName: "tool_tool_return_id"
            });
            const editFormFields = ref({"tool_tool_return_code":"","tool_tool_return_name":"","tool_code":"","tool_name":"","quantity_avail_remain":"","accepted_by_name":"","accepted_by_count":"","attr1":"","remark":""});
            const editFormOptions = ref([[{"title":"易损件借还编码","field":"tool_tool_return_code","type":"text"},
                               {"title":"易损件借还名称","required":true,"field":"tool_tool_return_name","type":"text"}],
                              [{"title":"易损件编码","required":true,"field":"tool_code","disabled":true,"type":"text"},
                               {"title":"易损件名称","required":true,"field":"tool_name","disabled":true,"type":"text"}],
                              [{"title":"易损件剩余次数","required":true,"field":"quantity_avail_remain","type":"number"},
                               {"dataKey":"人员选择下拉框","data":[],"title":"借用人员姓名","field":"accepted_by_name","type":"select"}],
                              [{"title":"借用次数","field":"accepted_by_count","type":"number"},
                               {"dataKey":"易损件借还状态","data":[],"title":"借还状态","field":"attr1","type":"select"}],
                              [{"title":"备注","field":"remark","colSize":12,"type":"text"}]]);
            const searchFormFields = ref({"tool_tool_return_code":"","tool_tool_return_name":"","attr1":""});
            const searchFormOptions = ref([[{"title":"易损件借还编码","field":"tool_tool_return_code","type":"like"},{"title":"易损件借还名称","field":"tool_tool_return_name","type":"like"},{"dataKey":"易损件借还状态","data":[],"title":"借还状态","field":"attr1","type":"select"}]]);
            const columns = ref([{field:'tool_tool_return_id',title:'易损件归还ID',type:'bigint',width:110,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'tool_tool_return_code',title:'易损件借还编码',type:'string',width:160,align:'left',sort:true},
                       {field:'tool_tool_return_name',title:'易损件借还名称',type:'string',width:220,require:true,align:'left'},
                       {field:'tool_code',title:'易损件编码',type:'string',width:160,readonly:true,require:true,align:'left'},
                       {field:'tool_name',title:'易损件名称',type:'string',width:220,readonly:true,require:true,align:'left'},
                       {field:'quantity_avail_remain',title:'易损件剩余次数',type:'int',width:120,require:true,align:'left'},
                       {field:'accepted_by_name',title:'借用人员姓名',type:'string',bind:{ key:'人员选择下拉框',data:[]},width:220,align:'left'},
                       {field:'accepted_by_count',title:'借用次数',type:'bigint',width:110,align:'left'},
                       {field:'status',title:'状态',type:'string',width:120,align:'left'},
                       {field:'remark',title:'备注',type:'string',width:220,hidden:true,align:'left'},
                       {field:'attr1',title:'借还状态',type:'string',bind:{ key:'易损件借还状态',data:[]},width:120,hidden:true,align:'left'},
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
