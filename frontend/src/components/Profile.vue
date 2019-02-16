<template>
  <v-layout row wrap>
    <v-flex xs12>
      <div class="profile-container">
        <img class="profile-image" :src="require('@/assets/profile-image.jpg')"/>
      </div>
    </v-flex>
    <v-flex xs12 mt-2>
      <div class="profile-container">
        <h1 class="profile-text">{{ account.name }}</h1>
      </div>
      <div class="profile-container">
        <h2 class="profile-text">{{ account.nameEng }}</h2>
      </div>
    </v-flex>
    <v-flex xs12 mt-3>
      <div class="profile-text profile-detail"><strong>{{ account.detail }}</strong></div>
    </v-flex>
  </v-layout>
  <!-- <v-divider light></v-divider> -->
</template>

<script>
export default {
  props: {
    account: Object
  }
}
</script>

<style scoped>
.profile-container{
  display: flex;
  justify-content: center;
}
.profile-image{
  height: 30vh;
  border-radius: 20%;
}
.profile-text{
  color: #707070;
}
.profile-detail{
  white-space:pre-wrap;
}
</style>
