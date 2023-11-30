<template>
  <div class="machinery-tree">
    <!-- 左边树结构 -->
    <div class="tree-left">
      <div class="tree-left-title">
        <span>易损件类型</span>
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
      <ToolReturn ref="table"></ToolReturn>
    </div>
  </div>
</template>

<script>
import ToolReturn from './tm_tool_return.vue'
export default {
  components: {
    ToolReturn
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
      // 调用ToolReturn组件中nodeClick方法
      this.$refs.table.$refs.grid.nodeClick(data.code);
    },
    getTreeData() {
      this.treeLoading = true
      this.http.get('api/tm_tool_type/GetAllTmToolTypeTree').then((result) => {
        console.log(result,'result');
        //记录原始数据
        this.orginData = result.data;
        let data = result.data.map(item => {
          return {
            id:item.tool_type_id,
            name:item.tool_type_name,
            code: item.tool_type_code,
          }
        })
        // 添加根节点数据
        this.data = [{
          id: -1,
          name: "易损件分类",
          code: -1,
          children: []
        }]
        this.data[0].children = data;
        this.treeLoading = false
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
