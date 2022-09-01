<template>
    <NavBarComponent></NavBarComponent>
    <component :is="currentComponent" @change-page="(name : string) => currentComponent = name"></component>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { MyQuizzesComponent, MyQuizzesCreateComponent, MyQuizzesAddQuestionsComponent,
MyQuizzesDetailsComponent, MyQuizzesEditComponent} from "@/components/MyAreaComponents/MyQuizzesComponents";

export default defineComponent({
    data(){
        return {
            currentComponent: 'MyQuizzesComponent'
        }
    },
    components: {
        MyQuizzesComponent,
        MyQuizzesCreateComponent,
        MyQuizzesAddQuestionsComponent,
        MyQuizzesDetailsComponent,
        MyQuizzesEditComponent
    }
})
</script>