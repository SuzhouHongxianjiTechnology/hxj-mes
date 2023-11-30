/*****************************************************************************************
**  Author:jxx 2022
**  QQ:283591387
**完整文档见：http://v2.volcore.xyz/document/api 【代码生成页面ViewGrid】
**常用示例见：http://v2.volcore.xyz/document/vueDev
**后台操作见：http://v2.volcore.xyz/document/netCoreDev
*****************************************************************************************/
//此js文件是用来自定义扩展业务代码，可以扩展一些自定义页面或者重新配置生成的代码
import * as dateUtil from "../../../uitils/dateFormatUtil.js";
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
  data() {
    return {
      prefixVal: "",
      submitTimeVal: "",
      submitTimeText: "",
      serialNumberVal: "",
      serialNumberNine: "",
    };
  },
  tableAction: '', //指定某张表的权限(这里填写表名,默认不用填写)
  buttons: { view: [], box: [], detail: [] }, //扩展的按钮
  methods: {
    getFormOption(field) {
      let option;
      this.editFormOptions.forEach(x => {
        x.forEach(item => {
          if (item.field == field) {
            option = item;
          }
        })
      })
      return option;
    },
    onInit() {  //框架初始化配置前，
      this.boxOptions.labelWidth = 120;

      this.editFormOptions[this.editFormOptions.length - 1].push({ //往新数组对象中添加新的属性 属性名对应属性值
        title: "编号预览",
        required: false,
        field: "number_preview",
        disabled: true,
        type: "label",
        inputStyle: { 'color': 'blue', 'font-size': '26px' }
      });

      var numReg = /^[0-9]*$/;
      var numRe = new RegExp(numReg);
      var prefix = this.getFormOption('prefix');
      var submitTime = this.getFormOption('time');
      var serialNumber = this.getFormOption('serialnumber');
      this.prefixVal = "";
      this.submitTimeVal = "";
      this.submitTimeText = "";
      this.serialNumberVal = "";
      this.serialNumberNine = "";
      prefix.onKeyPress = (val) => {
        if (val.toString() != '[object KeyboardEvent]') {
          this.prefixVal = val;
          if (this.currentAction == "update") {
            this.submitTimeVal = dateUtil.formatTimeStamp(Date.now(), this.editFormFields.time);
            submitTime.data.forEach((kv) => {
              //根据字典的值判断
              if (kv.key == this.editFormFields.time) {
                this.submitTimeText = kv.value
              }
            });
            this.serialNumberVal = "";
            this.serialNumberNine = ""
            for (var i = 0; i < this.editFormFields.serialnumber; i++) {
              if ((i + 1) == this.editFormFields.serialnumber) {
                this.serialNumberVal += "1"
              }
              else {
                this.serialNumberVal += "0"
              }
              this.serialNumberNine += "9"
            }
            serialNumber.extra = {
              text: "例：" + this.serialNumberNine,//显示文本
              style: "color:blue;",//指定样式
            }
          }
          this.editFormFields.number_preview = this.prefixVal + this.submitTimeVal + this.serialNumberVal;
          this.editFormFields.generative = this.prefixVal + this.submitTimeText + this.serialNumberVal + "~" + this.serialNumberNine;
        }
      };

      submitTime.onChange = (val, item) => {
        this.submitTimeVal = dateUtil.formatTimeStamp(Date.now(), val);
        submitTime.data.forEach((kv) => {
          //根据字典的值判断
          if (kv.key == val) {
            this.submitTimeText = kv.value
          }
        });
        if (this.currentAction == "update") {
          this.prefixVal = this.editFormFields.prefix;
          this.serialNumberVal = "";
          this.serialNumberNine = ""
          for (var i = 0; i < this.editFormFields.serialnumber; i++) {
            if ((i + 1) == this.editFormFields.serialnumber) {
              this.serialNumberVal += "1"
            }
            else {
              this.serialNumberVal += "0"
            }
            this.serialNumberNine += "9"
          }
          serialNumber.extra = {
            text: "例：" + this.serialNumberNine,//显示文本
            style: "color:blue;",//指定样式
          }
        };
        this.editFormFields.number_preview = this.prefixVal + this.submitTimeVal + this.serialNumberVal;
        this.editFormFields.generative = this.prefixVal + this.submitTimeText + this.serialNumberVal + "~" + this.serialNumberNine;
      };

      serialNumber.onKeyPress = (val) => {
        this.serialNumberVal = "";
        this.serialNumberNine = "";
        if (val.toString() != '[object KeyboardEvent]') {
          if (numRe.test(val)) {
            if (val > 10) {
              this.$Message.info('流水号不能大于10');
              return;
            }
            for (var i = 0; i < val; i++) {
              if ((i + 1) == val) {
                this.serialNumberVal += "1"
              }
              else {
                this.serialNumberVal += "0"
              }
              this.serialNumberNine += "9"
            }
          }
          else {
            this.$Message.error('请输入数字');
          }
        }
        serialNumber.extra = {
          text: "例：" + this.serialNumberNine,//显示文本
          style: "color:blue;",//指定样式
        }
        if (this.currentAction == "update") {
          this.prefixVal = this.editFormFields.Prefix;
          this.submitTimeVal = dateUtil.formatTimeStamp(Date.now(), this.editFormFields.time);
          submitTime.data.forEach((kv) => {
            //根据字典的值判断
            if (kv.key == this.editFormFields.time) {
              this.submitTimeText = kv.value
            }
          });
        }
        this.editFormFields.number_preview = this.prefixVal + this.submitTimeVal + this.serialNumberVal;
        this.editFormFields.generative = this.prefixVal + this.submitTimeText + this.serialNumberVal + "~" + this.serialNumberNine;
      };

    },
    onInited() {
      //框架初始化配置后
      //如果要配置明细表,在此方法操作
      //this.detailOptions.columns.forEach(column=>{ });
    },
    searchBefore(param) {
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
    modelOpenBefore(row) {
      this.prefixVal = "";
      this.submitTimeVal = "";
      this.submitTimeText = "";
      this.serialNumberVal = "";
      this.serialNumberNine = "";
    },
    modelOpenAfter(row) {
      if (this.currentAction == 'update') {
        var prefixVal = "";
        var submitTimeVal = "";
        var submitTimeText = "";
        var serialNumberVal = "";
        var serialNumberNine = "";
        var submitTime = this.getFormOption('time');
        var serialNumber = this.getFormOption('serialnumber');
        prefixVal = row.prefix;
        submitTimeVal = dateUtil.formatTimeStamp(Date.now(), row.time);
        submitTime.data.forEach((kv) => {
          //根据字典的值判断
          if (kv.key == row.submitTime) {
            submitTimeText = kv.value
          }
        });
        for (var i = 0; i < row.serialnumber; i++) {
          if ((i + 1) == row.serialnumber) {
            serialNumberVal += "1"
          }
          else {
            serialNumberVal += "0"
          }
          serialNumberNine += "9"
        }
        serialNumber.extra = {
          text: "例：" + serialNumberNine,//显示文本
          style: "color:blue;",//指定样式
        }
        prefixVal = prefixVal == null ? "" : prefixVal;
        this.editFormFields.number_preview = prefixVal + submitTimeVal + serialNumberVal;
      }
    }
  }
};
export default extension;
