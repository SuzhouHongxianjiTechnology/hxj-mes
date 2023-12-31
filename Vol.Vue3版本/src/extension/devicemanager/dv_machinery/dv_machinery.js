// import { ca, fa } from "element-plus/es/locale";

/*
 * @Description: 台账功能修改页面
 * @Author: AlanGao
 * @Date: 2023-11-22 23:29:49
 * @LastEditors: AlanGao
 * @LastEditTime: 2023-11-27 22:45:35
 */
let extension = {
  components: {
    //查询界面扩展组件
    gridHeader: '',
    gridBody: '',
    gridFooter: '',
    //新建、编辑弹出框扩展组件
    modelHeader: '',
    modelBody: '',
    modelFooter: ''
  },
  tableAction: '', //指定某张表的权限(这里填写表名,默认不用填写)
  buttons: { view: [], box: [], detail: [] }, //扩展的按钮
  methods: {
     //下面这些方法可以保留也可以删除
    onInit() {  //框架初始化配置前，
        //示例：在按钮的最前面添加一个按钮
        //   this.buttons.unshift({  //也可以用push或者splice方法来修改buttons数组
        //     name: '按钮', //按钮名称
        //     icon: 'el-icon-document', //按钮图标vue2版本见iview文档icon，vue3版本见element ui文档icon(注意不是element puls文档)
        //     type: 'primary', //按钮样式vue2版本见iview文档button，vue3版本见element ui文档button
        //     onClick: function () {
        //       this.$Message.success('点击了按钮');
        //     }
        //   });

        //示例：设置修改新建、编辑弹出框字段标签的长度
        // this.boxOptions.labelWidth = 150;
        // this.tableHeight = 400
        
    },
    onInited() {
      //框架初始化配置后
      //如果要配置明细表,在此方法操作
      //this.detailOptions.columns.forEach(column=>{ });
      // 设备维修,点检保养弹出框嵌套设备台账表的操作
      if(this.$route.path == "/dv_repair" || this.$route.path == "/dv_dss_record") {
        this.height = 400  //设置弹框表格高度
        this.single = true //设置弹框表格单选
        // 隐藏其他按钮，只保留查询
        this.buttons.forEach(item => {
          if(item.value != "Search") {
            item.hidden = true;
          }
        })
      }
    },
    nodeClick(machinery_type_id) {
      this.machinery_type_id = machinery_type_id;
      this.search()
    },
    searchBefore(param) {
      if(this.machinery_type_id) {
        param.wheres.push({
          name: 'machinery_type_id',
          value: this.machinery_type_id,
          displayType: 'cascader'
        })
      }
      //界面查询前,可以给param.wheres添加查询参数
      //返回false，则不会执行查询
      return true;
    },
    searchAfter(result) {
      //查询后，result返回的查询数据,可以在显示到表格前处理表格的值
      return true;
    },
    addBefore(formData) {
      //新建保存前formData为对象，包括明细表，可以给给表单设置值，自己输出看formData的值
      return true;
    },
    updateBefore(formData) {
      //编辑保存前formData为对象，包括明细表、删除行的Id
      return true;
    },
    rowClick({ row, column, event }) {
      //查询界面点击行事件
      // this.$refs.table.$refs.table.toggleRowSelection(row); //单击行时选中当前行;
    },
    modelOpenAfter(row) {
      //点击编辑、新建按钮弹出框后，可以在此处写逻辑，如，从后台获取数据
      //(1)判断是编辑还是新建操作： this.currentAction=='Add';
      //(2)给弹出框设置默认值
      //(3)this.editFormFields.字段='xxx';
      //如果需要给下拉框设置默认值，请遍历this.editFormOptions找到字段配置对应data属性的key值
      //看不懂就把输出看：console.log(this.editFormOptions)
      // 新增弹出框赋值
      if (this.currentAction == 'Add') {
        this.editFormFields.status = "STOP"
        this.editFormFields.machinery_ip = "127.0.0.1:102" 
      }
      this.editFormOptions.forEach(item => {
        item.forEach(x => {
          if (x.field == 'machinery_code') {
            x.placeholder = '请输入，忽略将自动生成'
          }
        });
      });
    }
  }
};
export default extension;
