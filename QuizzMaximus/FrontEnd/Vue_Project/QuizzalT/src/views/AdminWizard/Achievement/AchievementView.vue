<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { AchievementIndexComponent, AchievementCreateComponent, AchievementDetailsComponent,
AchievementEditComponent, AchievementDeleteComponent} from '@/components/AdminRelated/AchievementComponents'

export default{
    data(){
        return {
            componentName: 'AchievementIndexComponent'
        }
    },
    components: {
        AchievementIndexComponent,
        AchievementCreateComponent,
        AchievementDetailsComponent,
        AchievementEditComponent,
        AchievementDeleteComponent
    }
}
</script>