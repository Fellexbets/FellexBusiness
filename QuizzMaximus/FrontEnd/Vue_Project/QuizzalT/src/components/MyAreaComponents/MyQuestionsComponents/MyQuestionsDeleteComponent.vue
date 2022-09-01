<template>
    <div class="popupDelete">
         <div class="popupInnerDelete">
        <p class="text-danger">Delete this question?</p>
        <button @click="deleteQuestion()" class="buttonsCreate">Confirm</button>
        <button @click="cancel()" class="buttonsCreate">Cancel</button>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
    methods: {
        deleteQuestion(){
            this.$emit('delete', true);
        },
        cancel(){
            this.$emit('delete', false);
        }
    },
    emits: ["delete"]
})
</script>