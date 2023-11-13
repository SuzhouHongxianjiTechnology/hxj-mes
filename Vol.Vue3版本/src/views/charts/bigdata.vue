<!--
 * @Description: 大屏
 * @Author: AlanGao
 * @Date: 2023-11-12 22:31:41
 * @LastEditors: AlanGao
 * @LastEditTime: 2023-11-13 19:57:39
-->
<template>
  <div class="screenRoot">
    <!-- <dv-full-screen-container> -->
      <div class="screenTop">
        <div style="flex: 0 1 20%;">
          <dv-decoration8  style="width:100%;height:50%;" :key="indexKey"/>
        </div>
        <div style="flex: 0 1 60%">
          <dv-decoration5 style="width:100%;height:100%;" :key="indexKey"/>
        </div>
        <div style="flex: 0 1 20%">
          <dv-decoration8  :reverse="true" style="width:100%;height:50%;" :key="indexKey"/>
        </div>
        <div class="center-title">
          MES可视化大屏
        </div>
      </div>
      <div class="middle">
        <div style="width: 10%;height: 18%;box-sizing:border-box;" v-for="item in 10" :key="item">
            <dv-border-box12 :key="indexKey">
                <span style="color: #fff">123</span>
            </dv-border-box12>
        </div>
      </div>
    <!-- </dv-full-screen-container> -->
  </div>
</template>

<script>
export default {
  data() {
    return {
      indexKey: 0
    }
  },
  mounted() {
    window.addEventListener("resize",() => {
      this.$nextTick(() => {
        this.indexKey++
      })
    })
  }
}
</script>

<style lang="less">
.screenRoot {
  font-size: 16px;
  height: 100%;
  width: 100%;
  background-color: black;
  .screenTop {
    box-sizing: border-box;
    padding: 0 1px;
    display: flex;
    height: 6%;
    position: relative;
    width: 100%;
    justify-content: space-between;
    .center-title {
      font-size: 2em;
      position: absolute;
      color: #fff;
      left: 50%;
      top: 30%;
      transform: translate(-50%,-50%);
    }
  }
  .middle {
    width: 100%;
    height: 94%;
    /* border: 1px solid red; */
    display: flex;
    flex-direction: column;
    flex-wrap: wrap;
    justify-content: space-between;
    align-content:space-between;
  }
  .border-box-content {
    display: flex;
    justify-content: center;
    align-items: center;
  }
}



</style>