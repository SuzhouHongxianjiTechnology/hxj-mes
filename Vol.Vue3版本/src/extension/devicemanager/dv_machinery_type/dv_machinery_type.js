/*****************************************************************************************
**  Author:jxx 2022
**  QQ:283591387
**完整文档见：http://v2.volcore.xyz/document/api 【代码生成页面ViewGrid】
**常用示例见：http://v2.volcore.xyz/document/vueDev
**后台操作见：http://v2.volcore.xyz/document/netCoreDev
*****************************************************************************************/
//此js文件是用来自定义扩展业务代码，可以扩展一些自定义页面或者重新配置生成的代码

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
    onInit() { //框架初始化配置前，
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
      this.rowKey = "machinery_type_id";
    },
    loadTreeChildren(tree, treeNode, resolve) { //加载子节点
      let url = `api/dv_machinery_type/getTreeTableChildrenData?machineryId=${tree.machinery_type_id}`;
      this.http.post(url, {}).then(result => {
        resolve(result.rows)
      })
    },
    onInited() {
      let hasUpdate, hasDel, hasAdd;
      this.buttons.forEach((x) => {
        if (x.value == 'Update') {
          x.hidden = true;
          hasUpdate = true;
        } else if (x.value == 'Delete') {
          hasDel = true;
          x.hidden = true; //隐藏按钮
        } else if (x.value == 'Add') {
          x.type = "primary";
          hasAdd = true;
        }
      });
      if (!(hasUpdate || hasDel || hasAdd)) {
        return;
      }
      this.columns.push({
        title: '操作',
        field: '操作',
        width: 80,
        fixed: 'right',
        align: 'center',
        render: (h, { row, column, index }) => {
          return (
            <div>
            <el-button
            onClick={($e) => {
              this.addBtnClick(row)
            }}
            type="primary"
            link
            v-show={hasAdd}
            icon="Plus"
            >
            </el-button>
            <el-button
            onClick={($e) => {
              this.edit(row);
            }}
            type="success"
            link
            v-show={hasUpdate}
            icon="Edit"
            >
            </el-button>
            <el-tooltip
            class="box-item"
            effect="dark"
            content="删除"
            placement="top"
            >
            <el-button
            link
            onClick={($e) => {
              this.del(row);
            }}
            v-show={hasDel}
            type="danger"
            icon="Delete"
            >
            </el-button>
            </el-tooltip>
            </div>
            );
          }
        })
      },
      searchBefore(param) {
        if (!param.wheres.length) {
          param.value = 1;
        }
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
      addBtnClick(row) {
        //这里是动态addCurrnetRow属性记录当前点击的行数据,下面modelOpenAfter设置默认值
        this.addCurrnetRow = row;
        this.add();
      },
      addAfter() { //添加后刷新字典
        this.initDicKeys();
        return true;
      },
      updateAfter() {
        this.initDicKeys();
        return true;
      },
      delAfter(result) { //查询界面的表删除后
        this.initDicKeys();
        return true;
      },
      modelOpenAfter(row) {
        //点击行上的添加按钮事件
        if (this.addCurrnetRow) {
          
          //获取当前组织构架的所有父级id,用于设置新建时父级id的默认值
          
          //获取数据数据源
          let data = [];
          this.editFormOptions.forEach(options => {
            options.forEach(option => {
              if (option.field == 'parent_type_id') {
                data = option.orginData;
              }
            })
          })
          let parentIds = this.base.getTreeAllParent(this.addCurrnetRow.machinery_type_id, data).map(x => { return x.id });
          //设置编辑表单上级组织的默认值
          this.editFormFields.parent_type_id = parentIds;
          this.addCurrnetRow = null;
          
        }
      }
    }
  };
  export default extension;