<!--
 * @Description: 封装缩放的组件
 * @Author: AlanGao
 * @Date: 2023-11-21 20:29:34
 * @LastEditors: AlanGao
 * @LastEditTime: 2023-11-21 21:27:24
-->
<template>
  <div class="ScaleBox" ref="ScaleBox" :style="{width: width + 'px',height: height + 'px',transform: `scale(${scaleX},${scaleY})`,'transform-origin': 'left top',}">
    <slot></slot>
  </div>
</template>

<script>
export default {
    data() {
        return {
            scale: 1,
            width: document.documentElement.clientWidth - 200,
            height: document.documentElement.clientHeight -96,
            scaleX: null,
            scaleY: null
        }
    },
    mounted() {
        window.addEventListener('resize',() => {
            this.scaleX = (document.documentElement.clientWidth - 200) /this.width
            this.scaleY =  (document.documentElement.clientHeight - 96) / this.height 
        })
    },
    methods: {
        debounce(fn,delay = 500) {
            let timer;
            return function() {
                const th = this
                const args = arguments
                if(timer) {
                    clearTimeout(timer)
                }
                timer = setTimeout(function () {
                    timer = null
                    fn.apply(th,args)
                },delay)
            }
        }
    }
}
</script>

<style>

</style>