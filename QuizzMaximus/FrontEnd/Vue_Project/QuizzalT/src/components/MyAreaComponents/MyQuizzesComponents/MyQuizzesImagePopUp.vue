<template>
    <div class="popupImage">
         <div class="popupInnerImage">
            <div v-if="questionStore.getCurrentQuestion.questionImage">
                <img :src="`data:image/png;base64,${questionStore.getCurrentQuestion.questionImage}`"
                id="questionImage"/>
                <button @click="close()" id="buttonCloseImage">Close</button>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useQuestionStore } from "@/stores";

export default defineComponent({
    methods: {
        close(){
            this.$emit('closePopUp');
        }
    },
    computed: {
        ...mapStores(useQuestionStore)
    },
    emits: ["closePopUp"]
})
</script>