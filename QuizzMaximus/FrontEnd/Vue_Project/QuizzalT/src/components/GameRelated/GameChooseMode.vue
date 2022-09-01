<template>
    <div class="popupGame">
         <div class="popupInnerGame">
            <p class="text-danger">Please select Game Mode</p>
            <button @click="timedGame()" class="gameChooseButtons">Timed Game</button>
            <button @click="normalGame()" class="gameChooseButtons">Normal Game</button>
            <p><button @click="cancel()" class="gameChooseButtons">Cancel</button></p>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useAnswerStore, useQuestionStore } from "@/stores";

export default defineComponent({
    methods: {
        timedGame(){
            this.$emit('gameMode', 'timed');
        },
        normalGame(){
            this.$emit('gameMode', 'normal');
        },
        cancel(){
            this.$emit('gameMode', 'cancel');
        }
    },
    computed: {
        ...mapStores(useQuestionStore, useAnswerStore)
    },
    emits: ["gameMode"]
})
</script>