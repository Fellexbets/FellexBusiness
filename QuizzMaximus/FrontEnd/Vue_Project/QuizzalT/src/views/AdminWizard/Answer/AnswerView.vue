<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { AnswerIndexComponent, AnswerCreateComponent, AnswerDetailsComponent,
AnswerEditComponent, AnswerDeleteComponent} from '@/components/AdminRelated/AnswerComponents'

export default{
    data(){
        return {
            componentName: 'AnswerIndexComponent'
        }
    },
    components: {
        AnswerIndexComponent,
        AnswerCreateComponent,
        AnswerDetailsComponent,
        AnswerEditComponent,
        AnswerDeleteComponent
    }
}
</script>