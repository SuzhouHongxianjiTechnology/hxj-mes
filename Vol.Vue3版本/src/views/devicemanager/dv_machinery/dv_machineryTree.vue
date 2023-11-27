<template>
  <div class="machinery-tree">
    <!-- 左边树结构 -->
    <div class="tree-left">
      <div class="tree-left-title">
        <span>设备类型</span>
        <Refresh style="width: 20px;height: 20px;cursor: pointer;" @click="getTreeData"/>
      </div>
      <el-scrollbar style="flex:1;border-right: 1px solid #eee;">
        <el-tree
          node-key="id"
          default-expand-all
          highlight-current
          v-loading="treeLoading"
          :data="data"
          :props="defaultProps"
          :expand-on-click-node="false"
          @node-click="nodeClick"
        />
      </el-scrollbar>
    </div>
    <!-- 右边模版信息table数据 -->
    <div class="tree-right">
      <machinery ref="table"></machinery>
    </div>
  </div>
</template>

<script>
import machinery from './dv_machinery.vue'
export default {
  components: {
    machinery
  },
  data() {
    return {
      data: [],
      orginData: [], //原始数据
      defaultProps: {
        children: 'children',
        label: 'name' //树形结构显示名称的字段
      },
      treeLoading: false
    };
  },
  created() {
    // 获取设备类型
    this.getTreeData();
  },
  methods: {
    nodeClick(data) {//左侧树点击事件
      let ids = [];
      // 调用machinery组件中nodeClick方法
      this.$refs.table.$refs.grid.nodeClick(data.id);
    },
    getTreeData() {
      this.treeLoading = true
      this.http.get('api/dv_machinery_type/GetAllMachineryTypeTree').then((result) => {
        // console.log(result,'result');
        //记录原始数据
        this.orginData = result.data;
        let data = result.data.map(item => {
          return {
            id:item.machinery_type_id,
            name:item.machinery_type_name,
            parentId:item.parent_type_id,
            code: item.machinery_type_code,
          }
        })
        let childData = this.base.convertTree(data, (node, data, isRoot) => {});
        // 添加根节点数据
        this.data = [{
          id: -1,
          name: "设备分类",
          children: []
        }]
        this.data[0].children = childData;
        this.treeLoading = false
        //商品分类有数据时加载右边商品信息
        if (this.data.length) {
          //调用右边商品信息的查询
          this.$nextTick(() => {
            // this.nodeClick(this.data[0].id);
          });
        }
      });
    }
  }
};
</script>
<style lang="less" scoped>
.machinery-tree {
  position: absolute;
  display: flex;
  height: 100%;
  width: 100%;
  .tree-left {
    display: flex;
    flex-direction: column;
    width: 200px;
    padding-top: 15px;
    .tree-left-title {
      line-height: 25px;
      font-size: 15px;
      background: rgba(102, 177, 255, 0.058823529411764705);
      padding: 6px 16px;
      border: 1px solid #ececec;
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }
  .tree-right {
    flex: 1;
    width: 0;
  }
}
</style>
