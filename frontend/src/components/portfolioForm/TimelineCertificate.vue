<template>
  <v-flex xs12 md6>
    <!-- <v-img
      class="certificate"
      :src="certificateSrc"        
      position="left"
      contain
    ></v-img> -->
    <v-layout row wrap>
      <v-flex xs4 md3>
        <v-img
          class="certificate"
          :src="!this.certificate.src ? require('@/assets/university.svg') : this.certificate.src"        
          position="left"
          contain
        ></v-img>
      </v-flex>
      <v-flex xs8 md9 pa-2>
          {{ certificate.issuerName }}<br/>
          {{ certificate.detail + ' ' + certificate.type }}
      </v-flex>
    </v-layout>   
  </v-flex>
  <!-- <v-card color="white" class="mb-3">
    <v-layout row wrap>
      <v-flex xs4 md2>
        <v-img
          :src="certificateSrc"        
          position="left"
          contain
        ></v-img>
      </v-flex>
      <v-flex xs8 md10>
        <v-card-text class="px-2">
          {{ certificate.issuerName }}<br/>
          {{ certificate.detail + ' ' + certificate.type }}
        </v-card-text>
      </v-flex>
    </v-layout>   
  </v-card> -->
</template>

<script>
export default {
  props: {
    certificate: Object
  }
}
</script>

<style scoped>
.certificate{
  border: solid 1px #489DAB;
}
</style>

